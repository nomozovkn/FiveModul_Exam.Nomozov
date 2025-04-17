using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Bll.DTOs;

public class GetAllResponseModel
{
    public int TotalCount { get; set; }
    public List<ToDoItemGetDto> ToDoItemGetDtos { get; set; }
}
