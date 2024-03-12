using AlunosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosAPI.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly Contexto _context;

        public AlunoService(Contexto context)
        {
            _context = context;
        }

        public async Task CreateAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);

            await _context.SaveChangesAsync();
        }

        public async Task<Aluno> GetAluno(int id)
        {
            try
            {
                var aluno = await _context.Alunos.FindAsync(id);
                return aluno;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            IEnumerable<Aluno> alunos;

            if (!string.IsNullOrWhiteSpace(nome))
            {
                try
                {
                    return await _context.Alunos.Where(n => n.Nome.Contains(nome)).ToListAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                return await GetAlunos();
            }

        }

        public async Task UpdateAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
