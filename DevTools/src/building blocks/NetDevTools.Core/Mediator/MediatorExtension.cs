using Microsoft.EntityFrameworkCore;
using NetDevTools.Core.DomainObjects;

namespace NetDevTools.Core.Mediator
{
    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            await LancarEventos(mediator, ctx);
        }

        private static async Task LancarEventos(IMediatorHandler mediator, DbContext ctx)
        {
            var entities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var events = entities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            entities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = events
                .Select(async (domainEvent) =>
                {
                    await mediator.PublicarEvento(domainEvent);
                });


            await Task.WhenAll(tasks);
        }
    }
}
