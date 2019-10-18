using System;
using System.Linq;
using System.Transactions;
using System.Collections.Generic;
using PrimeTeamProjectsApi.Models;

namespace PrimeTeamProjectsApi.Business
{
    /// <summary>
    /// Classe de negócio.
    /// </summary>
    public class ProjetoBLL
    {
        #region Projeto
        /// <summary>
        /// Obtem os projetos cadastrados no sistema.
        /// </summary>
        /// <param name="codPrj">Código do projeto.</param>
        /// <param name="nomPrj">Nome do projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public List<Projeto> ObterProjetos(int codPrj, string nomPrj)
        {
            // DAL.
            ProjetoDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new ProjetoDAL();
                // Retornando consulta.
                return dal.ObterProjetos(codPrj, nomPrj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variável.
                dal = null;
            }
        }

        /// <summary>
        /// Obtem todos os dados do projeto.
        /// </summary>
        /// <param name="codPrj">Código do projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public ProjetoCompleto ObterProjetoCompleto(int codPrj)
        {
            // Criando um bloco de transação.
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // DAL.
                ProjetoDAL dal = null;
                // Conjunto Prime.
                ProjetoCompleto grupo = null;
                // Usuário.
                Usuario usuario = null;
                // Relacao por usuario.
                IEnumerable<RelacaoAUP> relacoesUsuario = null;
                // Relação por atividade.
                IEnumerable<RelacaoAUP> relacoesAtividade = null;
                // Tentativa.
                try
                {
                    // Instanciando dal.
                    dal = new ProjetoDAL();
                    // Criando um conjunto.
                    grupo = new ProjetoCompleto();
                    // Obtendo projeto.
                    grupo.projeto = dal.ObterProjetos(codPrj, null).FirstOrDefault();
                    // Obtendo tipos.
                    grupo.tipos = dal.ObterRelacaoPrjTip(codPrj);
                    // Obtendo relação Atividade x Usuário x Projeto.
                    grupo.relacoesAUP = dal.ObterRelacaoAUP(codPrj);
                    // Verificando se o projeto possuí relações.
                    if (grupo.relacoesAUP != null && grupo.relacoesAUP.Count > 0)
                    {
                        // Obtendo usuários de forma destinta. (Distinct)
                        relacoesUsuario = grupo.relacoesAUP.GroupBy(rel => rel.CODUSR).Select(rel => rel.First());
                        // Instanciando usuários.
                        grupo.usuarios = new List<Usuario>();
                        // Montando relação Atividade x Usuário.
                        foreach (RelacaoAUP relUsuario in relacoesUsuario)
                        {
                            // Criando usuário.
                            usuario = new Usuario()
                            {
                                CODUSR = relUsuario.CODUSR,
                                NOMUSR = relUsuario.NOMUSR
                            };
                            // Obtendo atividades pro usuário.
                            relacoesAtividade = grupo.relacoesAUP.Where(atv => atv.CODUSR == relUsuario.CODUSR && atv.CODPRJ == codPrj);
                            // Instanciando lista de atividade.
                            usuario.atividades = new List<Atividade>();
                            // Montando ativiadades atividades.
                            foreach (RelacaoAUP relAtividade in relacoesAtividade)
                                usuario.atividades.Add(new Atividade()
                                {
                                    CODATV = relAtividade.CODATV,
                                    NOMATV = relAtividade.NOMATV,
                                    DESATV = relAtividade.DESATV,
                                    DATINIATVSTR = relAtividade.DATINIATVSTR,
                                    DATFIMATVSTR = relAtividade.DATFIMATVSTR,
                                    TMPESTATV = relAtividade.TMPESTATV
                                });
                            // Adicionando usuário.
                            grupo.usuarios.Add(usuario);
                        }
                    }
                    // Retornando.
                    return grupo;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    // Finalizando transação.
                    scope.Dispose();
                    // Limpando variáveis.
                    dal = null;
                    grupo = null;
                    usuario = null;
                    relacoesUsuario = null;
                    relacoesAtividade = null;
                    // Limpando memória.
                    GC.Collect();
                }
            }
        }

        /// <summary>
        /// Inclui um novo projeto no sistema.
        /// </summary>
        /// <param name="projetoCompleto">Dados do projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool InserirProjeto(ProjetoCompleto projetoCompleto)
        {
            // Criando um bloco de transação.
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // DAL.
                ProjetoDAL dal = null;
                // Tentativa.
                try
                {
                    // Instanciando dal.
                    dal = new ProjetoDAL();
                    // Obtendo próximo código do projeto.
                    int prxCodPrj = dal.ObterProximoProjeto();
                    // Verificando se o código foi obtido.
                    if (prxCodPrj > 0)
                    {
                        // Inserindo projeto.
                        int proj = dal.InserirProjeto(projetoCompleto.projeto);
                        // Verificando se o projeto foi inserido.
                        if (proj > 0)
                        {
                            // Verificando se existe algum tipo para inserir.
                            if (projetoCompleto.tipos != null && projetoCompleto.tipos.Count > 0)
                            {
                                // Inserção Tipo.
                                int tip = 0;
                                // Inserindo relações com tipo.
                                foreach (TipoProjeto tipo in projetoCompleto.tipos)
                                {
                                    // Inserindo relação tipo.
                                    tip = dal.InserirRelacaoPrjTip(prxCodPrj, tipo.CODTIP);
                                    // Verificando se o tipo foi inserido.
                                    if (tip < 1) throw new Exception($"Erro ao inserir relação '{tipo.NOMTIP}'. Nenhuma linha foi inserida.");
                                }
                            }
                            // Verificando se existe alguma atividade e usuario para inserir no projeto.
                            if (projetoCompleto.relacoesAUP != null && projetoCompleto.relacoesAUP.Count > 0)
                            {
                                // Inserção Relação AUP.
                                int relAUP = 0;
                                // Inserido relações Atividade x Usuário x Projeto.
                                foreach (RelacaoAUP relacaoAUP in projetoCompleto.relacoesAUP)
                                {
                                    // Inserido relação Atividade x Usuário x Projeto.
                                    relAUP = dal.InserirRelacaoAUP(relacaoAUP.CODATV, prxCodPrj, relacaoAUP.CODUSR);
                                    // Verificando se a relação foi inserida.
                                    if (relAUP < 1) throw new Exception($"Erro ao inserir relação entre a atividade '{relacaoAUP.NOMATV}' e o usuário '{relacaoAUP.NOMUSR}' com este projeto. Nenhuma linha foi inserida.");
                                }
                            }
                        }
                        // Completando o scopo (commit).
                        scope.Complete();
                        // Retornando.
                        return true;
                    }
                    // Retornando.
                    return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    // Finalizando transação.
                    scope.Dispose();
                    // Limpando variável.
                    dal = null;
                }
            }
        }

        /// <summary>
        /// Atualiza dados do projeto.
        /// </summary>
        /// <param name="projetoCompleto">Dados do projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool AtualizarProjeto(ProjetoCompleto projetoCompleto)
        {
            // Criando um bloco de transação.
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // DAL.
                ProjetoDAL dal = null;
                // Tipos para remover / inserir.
                IEnumerable<TipoProjeto> tiposRemover = null, tiposInserir = null;
                // Relações AUP para remover / inserir.
                IEnumerable<RelacaoAUP> relacoesAUPRemover = null, relacoesAUPInserir = null;
                // Tentativa.
                try
                {
                    // Instanciando dal.
                    dal = new ProjetoDAL();
                    // Atualizando projeto.
                    int proj = dal.AtualizarProjeto(projetoCompleto.projeto);
                    // Verificando se o projeto foi atualizado.
                    if (proj > 0)
                    {
                        // Verificando se existe algum tipo.
                        if (projetoCompleto.tipos != null && projetoCompleto.tipos.Count > 0)
                        {
                            // Tipo retorno.
                            int tip = 0;

                            // Tipos para remover.
                            tiposRemover = projetoCompleto.tipos.Where(tipo => tipo.STATUSREL == 2);
                            // Verificando se existe algum tipo para remover.
                            if (tiposRemover.Count() > 0)
                            {                                
                                // Removendo relações com tipo.
                                foreach (TipoProjeto tipo in tiposRemover)
                                {
                                    // Removendo relação tipo.
                                    tip = dal.RemoverRelacaoPrjTip(projetoCompleto.projeto.CODPRJ, tipo.CODTIP);
                                    // Verificando se o tipo foi removido.
                                    if (tip < 1) throw new Exception($"Erro ao remover relação '{tipo.NOMTIP}'. Nenhuma linha foi removida.");
                                }
                            }

                            // Tipos para inserir.
                            tiposInserir = projetoCompleto.tipos.Where(tipo => tipo.STATUSREL == 1);
                            // Verificando se existe algum tipo para inserir.
                            if (tiposInserir.Count() > 0)
                            {
                                // Inserindo relações com tipo.
                                foreach (TipoProjeto tipo in tiposInserir)
                                {
                                    // Inserindo relação tipo.
                                    tip = dal.InserirRelacaoPrjTip(projetoCompleto.projeto.CODPRJ, tipo.CODTIP);
                                    // Verificando se o tipo foi inserido.
                                    if (tip < 1) throw new Exception($"Erro ao inserir relação '{tipo.NOMTIP}'. Nenhuma linha foi inserida.");
                                }
                            }
                        }

                        // Verificando se existe alguma atividade e usuario do projeto.
                        if (projetoCompleto.relacoesAUP != null && projetoCompleto.relacoesAUP.Count > 0)
                        {
                            // Relação AUP retorno.
                            int relAUP = 0;

                            // Relações para remover.
                            relacoesAUPRemover = projetoCompleto.relacoesAUP.Where(rel => rel.STATUSREL == 2);
                            // Verificando se existe alguma relação para remover.
                            if (relacoesAUPRemover.Count() > 0)
                            {
                                // Removendo relações Atividade x Usuário x Projeto.
                                foreach (RelacaoAUP relacaoAUP in relacoesAUPRemover)
                                {
                                    // Removendo relação Atividade x Usuário x Projeto.
                                    relAUP = dal.RemoverRelacaoAUP(relacaoAUP.CODATV, projetoCompleto.projeto.CODPRJ, relacaoAUP.CODUSR);
                                    // Verificando se a relação foi removida.
                                    if (relAUP < 1) throw new Exception($"Erro ao remover relação entre a atividade '{relacaoAUP.NOMATV}' e o usuário '{relacaoAUP.NOMUSR}' com este projeto. Nenhuma linha foi removida.");
                                }
                            }

                            // Relações para inserir.
                            relacoesAUPInserir = projetoCompleto.relacoesAUP.Where(rel => rel.STATUSREL == 1);
                            // Verificando se existe alguma relação para inserir.
                            if (relacoesAUPInserir.Count() > 0)
                            {
                                // Inserindo relações Atividade x Usuário x Projeto.
                                foreach (RelacaoAUP relacaoAUP in relacoesAUPInserir)
                                {
                                    // Inserindo relação Atividade x Usuário x Projeto.
                                    relAUP = dal.InserirRelacaoAUP(relacaoAUP.CODATV, projetoCompleto.projeto.CODPRJ, relacaoAUP.CODUSR);
                                    // Verificando se a relação foi inserida.
                                    if (relAUP < 1) throw new Exception($"Erro ao inserir relação entre a atividade '{relacaoAUP.NOMATV}' e o usuário '{relacaoAUP.NOMUSR}' com este projeto. Nenhuma linha foi inserida.");
                                }
                            }
                        }
                        // Completando o scopo (commit).
                        scope.Complete();
                        // Retornando.
                        return true;
                    }
                    // Retornando.
                    return false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    // Finalizando transação.
                    scope.Dispose();
                    // Limpando variáveis.
                    dal = null;
                    tiposRemover = null;
                    tiposInserir = null;
                    relacoesAUPRemover = null;
                    relacoesAUPInserir = null;
                    // Limpando memória.
                    GC.Collect();
                }
            }
        }
        #endregion

        #region TipoProjeto
        /// <summary>
        /// Obtem os tipos de projeto.
        /// </summary>
        /// <param name="codTip">Código do tipo de projeto.</param>
        /// <param name="nomTip">Nome do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public List<TipoProjeto> ObterTiposProjeto(int codTip, string nomTip)
        {
            // DAL.
            ProjetoDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new ProjetoDAL();
                // Retornando consulta.
                return dal.ObterTiposProjeto(codTip, nomTip);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variável.
                dal = null;
            }
        }

        /// <summary>
        /// Insere um novo tipo de projeto.
        /// </summary>
        /// <param name="tipoProjeto">Objeto do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool InserirTipoProjeto(TipoProjeto tipoProjeto)
        {
            // DAL.
            ProjetoDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new ProjetoDAL();
                // Executando.
                return dal.InserirTipoProjeto(tipoProjeto) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variável.
                dal = null;
            }
        }

        /// <summary>
        /// Atualiza os dados do tipo de projeto.
        /// </summary>
        /// <param name="tipoProjeto">Objeto do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool AtualizarTipoProjeto(TipoProjeto tipoProjeto)
        {
            // DAL.
            ProjetoDAL dal = null;
            // Tentativa.
            try
            {
                // Instanciando dal.
                dal = new ProjetoDAL();
                // Executando.
                return dal.AtualizarTipoProjeto(tipoProjeto) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Limpando variável.
                dal = null;
            }
        }

        /// <summary>
        /// Deleta o tipo de projeto.
        /// </summary>
        /// <param name="tipoProjeto">Objeto do tipo de projeto.</param>
        /// <returns></returns>
        /// <remarks>Leon Denis Paiva e Silva [PrimeTeam]</remarks>
        public bool RemoverTipoProjeto(TipoProjeto tipoProjeto)
        {
            // Criando um bloco de transação.
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // DAL.            
                ProjetoDAL dal = null;
                // Tentativa.
                try
                {
                    // Instanciando dal.
                    dal = new ProjetoDAL();
                    // Obtendo projetos do tipo.
                    int projetos = dal.VerificarProjetos(tipoProjeto.CODTIP);
                    // Se o usuário possuí atividades impedir desativação do mesmo.
                    if (projetos > 0)
                        throw new Exception($"Não foi possível remover o tipo '{tipoProjeto.NOMTIP}' pois, o mesmo já está cadastrado em {projetos} projeto(s).");
                    // Executando.
                    int dstUsr = dal.RemoverTipoProjeto(tipoProjeto.CODTIP);
                    // Completando o scopo (commit).
                    scope.Complete();
                    // Retornando.
                    return dstUsr > 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    // Finalizando transação.
                    scope.Dispose();
                    // Limpando variável.
                    dal = null;
                }
            }
        }
        #endregion

    }
}