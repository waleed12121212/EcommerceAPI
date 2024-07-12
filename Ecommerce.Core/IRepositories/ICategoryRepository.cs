﻿using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.IRepositories
{
    public interface ICategoryRepository
    {
        public IEnumerable<Categories> GetAllCategory( );
        public Products GetById(int id);
        public void CreateCategory(Categories model);
        public void UpdateCategory(Categories model);
        public void DeleteCategory(int id);
    }
}
