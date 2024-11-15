using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tni_back.Entities.Constants
{
    public static class ConstantsEntity
    {
        public static bool ACTIVE = true;

        public static bool NOT_DELETED = false;
        public static bool DELETED = true;

        public static Guid ADD_TO_CART = Guid.Parse("2efbadfb-5288-4800-aa9c-e8e80b7c0a0e");
        public static Guid REMOVE_FROM_CART = Guid.Parse("799ee589-5237-4ecd-9f18-5214d8a738e5");
        public static Guid CHECK_OUT = Guid.Parse("99155814-ce11-46e0-96a4-600cb5061b6f");
    }
}