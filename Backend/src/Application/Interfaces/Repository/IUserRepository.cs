using Domain;

namespace Application;

public interface IUserRepository: 
    IUserRepositoryGetByEmail, 
    IUserRepositoryGetById;
