namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string resourceType, string resourceId) : Exception($"The {resourceType} with id {resourceId} was not found.");