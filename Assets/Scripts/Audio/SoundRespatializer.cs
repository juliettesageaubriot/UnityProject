using System;
using Global;
using Player;
using UnityEngine;
using Utils;

namespace Audio
{
    public class SoundRespatializer : MonoBehaviour
    {
        [SerializeField] private Pathfinder pathfinder;
        [SerializeField] private PlayerPositionData player;
        [SerializeField] private GameObject soundEmitter;

        private AnimationCurve _soundCurve;
        private AudioSource _audioSource;
    
        public void Update()
        {
            var playerFromPosition = player.Position;
            
            playerFromPosition.x = player.Direction.x > 0 ? Mathf.Floor(playerFromPosition.x) : Mathf.Ceil(playerFromPosition.x);
            playerFromPosition.y = player.Direction.y > 0 ? Mathf.Floor(playerFromPosition.y) : Mathf.Ceil(playerFromPosition.y);
            var (direction, distance) = GetSoundDirection(playerFromPosition);
            float finalDistance = distance;
            
            
            if (direction.magnitude < 1f && ((Vector2)transform.position - player.Position).magnitude > 1f)
            {
                direction += (Vector2) transform.position - player.Position;
                direction = direction.normalized;
            }

            if (player.IsMoving)
            {
                var progressionVector = pathfinder.DistanceMap.DistanceFromNearestBottomLeftCell(player.Position);
                var progression = progressionVector.x + progressionVector.y;
                progression = (player.Direction.x < 0 || player.Direction.y < 0) && progression != 0f ? 1 - progression : progression;

                var nextCellPosition = playerFromPosition + player.Direction * pathfinder.DistanceMap.CellSize;
                
                var (nextDirection, nextDistance) = GetSoundDirection(nextCellPosition);
                
                if (nextDirection.magnitude < 1f && ((Vector2)transform.position - player.Position).magnitude > 1f)
                {
                    nextDirection += (Vector2) transform.position - player.Position;
                    nextDirection = nextDirection.normalized;
                }

                direction = Vector2.Lerp(nextDirection, direction, 1f - progression);
                finalDistance = Mathf.Lerp(nextDistance, distance, 1f - progression);
            }
            
            soundEmitter.transform.position = player.CenterPosition + direction * (1f + finalDistance);
        }

        private (Vector2, int) GetSoundDirection(Vector2 pos)
        {
            var hereDistance = pathfinder.DistanceMap.Get(player.Position);
                if (hereDistance == 0) return (Vector2.zero, 0);
            var (neighbours, distance) = pathfinder.GetClosestNeigbours(pos);
            var direction = Vector2.zero;
            foreach (var neighbour in neighbours)
                direction += neighbour - pos;
            
            return (direction.normalized, distance);
        }
    }
}
