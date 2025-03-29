using Application;
using Application.DTO.Users;
using Application.UseCases;
using Application.UseCases.Commands.Users;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.Users;

public class EfChangeProfilePhotoCommand : EfUseCase, IChangeProfilePhotoCommand
{
    private readonly IApplicationActor _user;
    public EfChangeProfilePhotoCommand(BookingContext context, IApplicationActor user)
        : base(context)
    {
        _user = user;
    }

    public int Id => 33;

    public string Name => nameof(EfChangeProfilePhotoCommand);

    public void Execute(string fileName)
    {
        var user = Context.Users.FirstOrDefault(x => x.Id == _user.Id);
        if(user == null)
        {
            throw new Exception("User not found!");
        }

        user.Avatar = fileName;
        Context.SaveChanges();
    }
}
