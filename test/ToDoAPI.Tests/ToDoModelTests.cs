using ToDoAPI.Models;

namespace ToDoAPI.Tests;

public class ToDoModelTests
{
    [Fact]
    public void Test1()
    {
        // arrange
        var todo = new ToDoItem
        { 
            Name = "Complete this section"
        };

        // act
        todo.Name = "Complete Azure PipeLine";

        // assert
        Assert.Equal("Complete Azure PipeLine", todo.Name);
    }
}