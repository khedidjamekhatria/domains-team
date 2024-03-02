using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLAB.domain.Storages;

namespace GLAB.infra.Storages
{
    public interface ILabStorage
    {

        Task<List<Laboratory>> SelectLaboratories();

        Task insertLaboratory(Laboratory laboratory);

        Task updateLaboratory(Laboratory laboratory);

        Task deleteLaboratory(String id);

    }
}
