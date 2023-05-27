using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharBase
{
    public void TakeDamage(int damage);
    public int SkillAttack();
    public void MoveTo();

    public IEnumerator OnDie();
}

public interface ImageChanger
{
    public void ImageChange();
}

public interface UIActive
{
    public void UIOpen();
    public IEnumerator UIClose();

    public void StartUIClose();
}