using System;
using System.Collections.Generic;

namespace CompanyApi.Models.Posgres;

public partial class SelectStatistic
{
    public long? AllTasksCnt { get; set; }

    public long? TasksCmpltOnTimeCnt { get; set; }

    public long? TasksCmpltOutTimeCnt { get; set; }

    public long? TasksNotCmpltOutTimeCnt { get; set; }

    public long? TasksNotCmpltOnTimeCnt { get; set; }
}
