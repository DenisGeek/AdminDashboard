namespace Application;

public interface IRateRepository:
    IRateRepositoryGet,
    IRateRepositoryUpdate,
    IRateRepositoryGetAll;
