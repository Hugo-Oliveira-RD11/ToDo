namespace tests.Controllers;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServicesMock;
    private readonly UserController _userController;
    public UserControllerTests()
    {
        _userServicesMock = new Mock<IUserService>();
        _userController = new UserController(_userServicesMock.Object);
    }

    [Fact]
    public async Task GivenUserDoesNotExist_WhenUserCreatesAccount_ThenUserIsCreated()
    {
        //arrange
        var user = new User{
            Name = "hugo",
            Email = "hugo@boss.com",
            Password = "12345"
        };
        var resultExpected = new UserDTO(Guid.NewGuid(), "hugo", "hugo@boss.com");
        _userServicesMock.Setup(service => service.CreateUser(user)).ReturnsAsync(resultExpected);

        //act
        var result = await _userController.CreateUser(user);

        //assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actualUserDTO = Assert.IsType<UserDTO>(okResult.Value);

        Assert.Equal(user.Email,actualUserDTO.Email);
        Assert.Equal(user.Name,actualUserDTO.Name);
        Assert.NotEqual(Guid.Empty, actualUserDTO.Id);
    }
}