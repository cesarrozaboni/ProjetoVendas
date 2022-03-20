using Infra.Departament;
using ProjetoVendas.Models.Departament;

namespace Services.Departament
{
    public class DepartamentService
    {
        public int CountDepartament()
        {
            DepartamentDal departament = new DepartamentDal();

            return departament.CountDepartament();
        }

        public int InsertDepartament(DepartamentModel model)
        {
            DepartamentDal departament = new DepartamentDal();

            return departament.InsertDepartament(model);
        }

        public int UpdateDepartament(DepartamentModel model)
        {
            DepartamentDal departament = new DepartamentDal();

            return departament.UpdateDepartament(model);
        }

        public int DeleteDepartament(int id)
        {
            DepartamentDal departament = new DepartamentDal();

            return departament.DeleteDepartament(id);
        }

        public DepartamentModel GetDepartamentForId(int id)
        {
            DepartamentDal departament = new DepartamentDal();

            return departament.GetDepartamentForId(id);
        }

        public List<DepartamentModel> GetAllDepartament()
        {
            DepartamentDal departament = new DepartamentDal();

            return departament.GetAllDepartament().OrderBy(x => x.Name).ToList();
        }
    }
}
