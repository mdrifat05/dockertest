namespace MongoDbDocker.Dtos;

public record PeopleDto(Guid Id, string FirstName, string LastName, int Age);
public record CreatePeopleDto(string FirstName, string LastName, int Age);

