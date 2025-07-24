using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(menuName = "Configs/GameConfig")]
	public class GameConfig: ScriptableObject
	{
		public int Rows = 3;
		public int Columns = 3;
		public int Mines = 3;
		
		private void OnValidate()
		{
			Rows = Mathf.Max(1, Rows);
			Columns = Mathf.Max(1, Columns);

			Mines = Mathf.Clamp(Mines, 0, Rows * Columns - 1);
		}
	}
}