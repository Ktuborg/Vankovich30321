namespace GR30321.Blazor.Services
{
    public interface IPictureService<T> where T : class
    {
        event Action ListChanged; //должно сообщать о том, что  список объектов изменился
        
        // Список объектов
        IEnumerable<T> Pictures { get; }
        // Номер текущей страницы
        int CurrentPage { get; }
        // Общее количество страниц
        int TotalPages { get; }

        // Получение списка объектов
        Task GetPictures(int pageNo = 1, int pageSize = 3);
    }
}
