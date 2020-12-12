namespace DocJc.Mapper
{
    using Model.Models;
    using Model.ViewModel;

    public class BaseEntityMapper
    {
        public BaseEntityViewModel FromEntityToViewModel(BaseEntity baseEntity)
        {
            return new BaseEntityViewModel
            {
                ID = baseEntity.ID,
                IsSelected = false,
                Name = baseEntity.Name
            };
        }
        public BaseEntity FromViewModelToEntity(BaseEntityViewModel baseEntity)
        {
            return new BaseEntity
            {
                ID = baseEntity.ID,
                Name = baseEntity.Name
            };
        }
    }
}
