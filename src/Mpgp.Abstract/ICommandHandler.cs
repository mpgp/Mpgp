// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Mpgp.Abstract
{
    public interface ICommandHandler<in TCommand> : System.IDisposable
        where TCommand : ICommand
    {
        System.Threading.Tasks.Task<int> Execute(TCommand command);
    }
}
