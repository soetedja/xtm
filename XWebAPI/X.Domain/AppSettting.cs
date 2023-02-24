using System;

namespace X.Domain
{
    public class AppSetting: BaseEntity
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
