// Application/Exceptions/NotFoundException.cs
namespace Stockly.Application.Exceptions;

public class NotFoundException(string message) : Exception(message);
