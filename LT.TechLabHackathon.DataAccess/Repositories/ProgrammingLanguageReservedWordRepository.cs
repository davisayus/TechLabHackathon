﻿using LT.TechLabHackathon.DataAccess.Repositories.Generic;
using LT.TechLabHackathon.DataAccess.SqlServerContext;
using LT.TechLabHackathon.Domain.Contracts;
using LT.TechLabHackathon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.DataAccess.Repositories
{
    public class ProgrammingLanguageReservedWordRepository : GenericRepository<ProgrammingLanguageReservedWord>, IProgrammingLanguageReservedWordRepository
    {
        public ProgrammingLanguageReservedWordRepository(SqlContext context) : base(context)
        {
        }
    }
}
