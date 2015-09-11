using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKeCRM.Common.Icons
{
	public class IconParameter
	{
		public Guid? FormFK { get; set; }

		public string PageKey { get; set; }

		public Guid? StudyId { get; set; }

		public int FormStatus { get; set; }
	}
}
