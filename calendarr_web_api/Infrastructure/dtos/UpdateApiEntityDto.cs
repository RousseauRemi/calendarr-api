using calendarr_web_api.Domain;

namespace calendarr_web_api.Infrastructure.dtos;

public class UpdateApiEntityDto
{
    public string Url { get; set; }
    public string Name { get; set; }
    public string ApiKey { get; set; }
    public ApiTypeEnum ApiType { get; set; }
    public long Color { get; set; }

    public string OldName { get; set; }
    
}