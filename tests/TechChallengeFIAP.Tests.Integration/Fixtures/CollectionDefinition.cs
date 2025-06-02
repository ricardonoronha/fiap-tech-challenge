using TechChallengeFIAP.Tests.Integration.Fixtures;
using Xunit;

[CollectionDefinition("SqlServerTestCollection")]
public class SqlServerTestCollection : ICollectionFixture<SqlServerTestContainerFixture>
{ }