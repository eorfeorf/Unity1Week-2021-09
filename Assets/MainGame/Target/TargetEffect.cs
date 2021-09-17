using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class TargetEffect : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;
    
    private VisualEffect vfx;
    private Player player;
    private Vector3 playerPos;

    private void Awake()
    {
        vfx = GetComponent<VisualEffect>();
        player = FindObjectOfType<Player>(); 
        
        vfx.SetFloat("Lifetime", lifetime);
        player.Position.Subscribe(pos =>
        {
            playerPos = pos;
        }).AddTo(this);
    }

    public async UniTask PlayDestroyEffect(CancellationToken ct)
    {
        vfx.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(lifetime), cancellationToken: ct);
    }

    private void Update()
    {
        vfx.SetVector3("PlayerDirection", (playerPos - transform.root.position).normalized);
    }
}
