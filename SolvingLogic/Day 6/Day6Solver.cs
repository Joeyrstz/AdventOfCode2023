namespace SolvingLogic.Day_6;

public static class Day6Solver
{
    public static int SolveTask1()
    {
        var timeArray = new int[] { 56, 97, 77, 93 };
        var distanceArray = new int[] { 499, 2210, 1097, 1440 };
        //var timeArray = new int[] { 7, 15, 30 };
        //var distanceArray = new int[] { 9, 40, 200 };
        

        var result = 1;
        
        for (int i = 0; i < timeArray.Length; i++)
        {
            var time = timeArray[i];
            var distance = distanceArray[i];
            var counter = 0;

            for (int j = 1; j < time; j++)
            {
                var reachedDistance = j * (time-j);
                if(reachedDistance > distance)
                {
                    counter++;
                }
            }
            result *= counter;
            
        }


        return result;
    }

    public static long SolveTask2()
    {
        var time = 56977793;
        var distance = 499221010971440;
        

        var counter = 0;

        for (long j = 1; j < time; j++)
        {
            var reachedDistance = j * (time - j);
            if (reachedDistance > distance)
            {
                counter++;
            }
        }


        return counter;
    }
}