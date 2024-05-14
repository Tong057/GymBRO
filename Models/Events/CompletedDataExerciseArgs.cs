using System;
using GymBro.Models.Entities;

namespace GymBro.Models.Events
{
	public class CompletedDataExerciseArgs : EventArgs
	{
		public ExerciseStatus ExerciseStatus { get; set; }

		public CompletedDataExerciseArgs(ExerciseStatus exerciseStatus)
		{
			ExerciseStatus = exerciseStatus;
		}
	}
}

