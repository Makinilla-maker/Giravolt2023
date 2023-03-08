using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("PlayerCopy", transform.position, Quaternion.identity);
        ViewFinder view = spawnedPlayerPrefab.GetComponent<ViewFinder>();
        //Crear script nuevo que se añade al player con una referencia al photonview (componente) ya ahi mirar el isMine 

        view.CheckPhotonViews(spawnedPlayerPrefab);
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
