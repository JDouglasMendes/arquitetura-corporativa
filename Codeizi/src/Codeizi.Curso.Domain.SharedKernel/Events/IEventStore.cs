namespace Codeizi.Curso.Domain.SharedKernel.Events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent)
            where T : Event;
    }
}
