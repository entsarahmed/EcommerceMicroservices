using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.Commands
{
    public class DeleteBasketByUserNameCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public DeleteBasketByUserNameCommand(string userName)
        {
            UserName = userName;
        }
    }
}
