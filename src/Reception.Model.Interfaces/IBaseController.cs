﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Reception.Model.Interface
{
    public interface IBaseController
    {
        public Task<IActionResult> Get(int id);
        public Task<IActionResult> Search(string searchText);
    }
}