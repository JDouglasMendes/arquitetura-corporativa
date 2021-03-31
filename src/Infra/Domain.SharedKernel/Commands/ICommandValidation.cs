using System.Threading.Tasks;

namespace Domain.SharedKernel.Commands
{
    public interface ICommandValidation<T>
    {
        Task<bool> IsValid(T validation);
    }
}