// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Mvc;

namespace Mpgp.RestApiServer.Controllers
{
    public class UserController : Controller
    {
        public ActionResult GetAvatar(string login)
        {
            return Ok("657");
        }
    }
}