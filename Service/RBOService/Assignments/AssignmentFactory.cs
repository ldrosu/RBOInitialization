using RBOService.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RBOService.Initializations.Assignments
{
    class AssignmentFactory
    {
        public static IAssignment Create(SourceTypeEnum source)
        {
            switch (source)
            {
                case SourceTypeEnum.Value:
                    return new ValueAssignment();
                case SourceTypeEnum.Random:
                    return new RandomAssignment();
                case SourceTypeEnum.Sequence:
                    return new SequenceAssignment();
                case SourceTypeEnum.Array:
                    return new ArrayAssignment();
                default:
                    throw new InvalidDataException();
            }

        }
    }
}
