using MyLuck.Infrastructure.Models;

namespace MyLuck.Infrastructure.Features.EuroDreams;

public sealed class EuroDreams(int[] numbers, int[] specialNumbers, DateOnly drawDay) 
    : DrawResult(numbers, specialNumbers, drawDay)
{
    
}