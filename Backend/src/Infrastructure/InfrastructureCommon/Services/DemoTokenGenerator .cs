using Application;
using Domain;

namespace InfrastructureCommon;

internal class DemoTokenGenerator : ITokenGenerator
{
    public string Generate(User user)
    {
        return "demo"; // По требованиям ТЗ
    }
}