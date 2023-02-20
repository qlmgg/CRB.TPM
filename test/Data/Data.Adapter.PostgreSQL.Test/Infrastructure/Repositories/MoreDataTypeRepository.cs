﻿using Data.Adapter.PostgreSQL.Test.Domain.Json;
using Data.Adapter.PostgreSQL.Test.Domain.MoreDataType;
using CRB.TPM.Data.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Adapter.PostgreSQL.Test.Infrastructure.Repositories;

internal class MoreDataTypeRepository : RepositoryAbstract<MoreDataTypeEntity>, IMoreDataTypeRepository
{
}
