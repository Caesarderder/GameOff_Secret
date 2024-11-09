
using UnityEngine;

public interface IUpdate
{
	void Register(MonoBehaviour mono);
	void Update();
}

public interface IFixedUpdate
{
	void Register(MonoBehaviour mono);
	void FiexedUpdate();
}
