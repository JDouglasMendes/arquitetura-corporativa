using Codeizi.Curso.Infra.CrossCutting.Configuration;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;

namespace Codeizi.Curso.RH.infra.Data.EventSource.Context
{
    public sealed class DocumentStoreHolder : IDisposable
    {
        private ICodeiziConfiguration CodeiziConfiguration { get; }
        private readonly Lazy<IDocumentStore> LazyStore;
        private readonly IDocumentStore Store;

        public DocumentStoreHolder(ICodeiziConfiguration codeiziConfiguration)
        {
            CodeiziConfiguration = codeiziConfiguration;
            LazyStore =
            new Lazy<IDocumentStore>(() =>
            {
                var store = new DocumentStore
                {
                    Urls = new[] { CodeiziConfiguration.ConnectionStringRavenDB },
                    Database = CodeiziConfiguration.DatabaseRavenDB
                };
                return store.Initialize();
            });
            Store = LazyStore.Value;
            Session = Store.OpenSession();
        }

        public IDocumentSession Session { get; }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}