using OrderService.Model.Entities;

namespace OrderService.Model
{
    public class UpdateRequestModel: CreateRequestModel
    {
        public int Id { get; set; }

        public RequestStatus RequestStatus { get; set; }

    }
}
