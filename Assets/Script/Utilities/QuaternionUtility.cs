using UnityEngine;

/// <summary>
/// Classe che contiene funzioni di utility sui quaternioni
/// </summary>
public static class QuaternionUtility
{
    /// <summary>
    /// Funzione che ritorna l'angolo in euler passando l'angolo in inspector
    /// </summary>
    /// <param name="_euler"></param>
    /// <returns></returns>
    public static float GetInspectorAngleByEuler(float _euler)
    {
        float inspectorAngle = _euler;
        inspectorAngle %= 360;
        return inspectorAngle = inspectorAngle > 180 ? inspectorAngle - 360 : inspectorAngle;
    }

    /// <summary>
    /// Funzione che ritorna l'angolo in inspector passando l'euler
    /// </summary>
    /// <param name="_inspectorAngle"></param>
    /// <returns></returns>
    public static float GetEulerByInspectorAngle(float _inspectorAngle)
    {
        float eulerAngle = _inspectorAngle;
        if (eulerAngle >= 0)
            return eulerAngle;

        eulerAngle = -eulerAngle % 360;

        return 360 - eulerAngle;
    }

    /// <summary>
    /// Funzione che ritorna la rotazione locale passandogli una rotazione globale
    /// </summary>
    /// <param name="_transform"></param>
    /// <param name="_targetRotation"></param>
    /// <returns></returns>
    public static Quaternion GetLocalRotationAtRotation(Transform _transform, Quaternion _targetRotation)
    {
        return Quaternion.Inverse(_transform.parent.rotation) * _targetRotation;
    }

    /// <summary>
    /// Funzione che ritorna gli euler locali passandogli una rotazione globale
    /// </summary>
    /// <param name="_transform"></param>
    /// <param name="_targetRotation"></param>
    /// <returns></returns>
    public static Vector3 GetLocalEulerAtRotation(Transform _transform, Quaternion _targetRotation)
    {
        return GetLocalRotationAtRotation(_transform, _targetRotation).eulerAngles;
    }

    /// <summary>
    /// Funzione che ritorna una rotazione clampata su tutti gli assi 
    /// </summary>
    /// <param name="_newRotation"></param>
    /// <param name="_currentRotation"></param>
    /// <param name="_maxAngle"></param>
    /// <returns></returns>
    public static Quaternion ClampRotation(Quaternion _newRotation, Quaternion _currentRotation, float _maxAngle)
    {
        Vector3 currentEulers = _currentRotation.eulerAngles;
        Vector3 newEulers = _newRotation.eulerAngles;
        Vector3 clampedEulers = new Vector3();

        float xDeltaAngle = newEulers.x - currentEulers.x;
        if (Mathf.Abs(xDeltaAngle) > _maxAngle)
            clampedEulers.x = currentEulers.x + xDeltaAngle;
        else
            clampedEulers.x = newEulers.x;

        float yDeltaAngle = newEulers.y - currentEulers.y;
        if (Mathf.Abs(yDeltaAngle) > _maxAngle)
            clampedEulers.y = currentEulers.y + yDeltaAngle;
        else
            clampedEulers.y = newEulers.y;

        float zDeltaAngle = newEulers.z - currentEulers.z;
        if (Mathf.Abs(zDeltaAngle) > _maxAngle)
            clampedEulers.z = currentEulers.z + zDeltaAngle;
        else
            clampedEulers.z = newEulers.z;

        return Quaternion.Euler(clampedEulers);
    }   
}
