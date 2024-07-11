namespace Trains;

public interface ITrainsStarter
{
    /// <summary>
    /// Calculate the best list of movements to move all destination cars to the train line.
    /// </summary>
    /// <param name="trainLines">Starting train lines.</param>
    /// <param name="destination">Destination's char.</param>
    /// <returns>Textual representation of the list of car movements (ex: A,2,0;AA,1,0)</returns>
    /// <remark>Returns string.Empty when no solution exists.</remark>
    string Start(string[] trainLines, char destination);
}