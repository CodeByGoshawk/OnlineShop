namespace OnlineShop.Domain.Frameworks.Abstracts;

internal interface IMainEntity : IEntity<Guid>, IDbSetEntity, ICodedEntity<string>,IModifiedEntity,ICreatedEntity,ISoftDeletedEntity
{

}
