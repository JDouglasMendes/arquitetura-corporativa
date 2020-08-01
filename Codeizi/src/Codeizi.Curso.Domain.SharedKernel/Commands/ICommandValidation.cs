using System.Threading.Tasks;

namespace Codeizi.Curso.RH.Domain.SharedKernel.Commands
{
    public interface ICommandValidation<T>
    {
        Task<bool> IsValid(T validation);
    }
}