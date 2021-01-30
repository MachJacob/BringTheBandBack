using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : MonoBehaviour
{
    public struct Pos   //recorded positions
    {
        public Vector2 pos;
        public bool flip;
    }

    public struct Memb  //important stuff for band members
    {
        public Transform tra;
        public SpriteRenderer spr;
    }

    private List<Pos> positions;
    private SpriteRenderer spr;
    private List<Memb> bandMem;

    void Start()
    {
        positions = new List<Pos>();
        spr = GetComponent<SpriteRenderer>();
        bandMem = new List<Memb>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Pos temp = new Pos  //add current position to back of list
        {
            pos = transform.position,
            flip = spr.flipX
        };
        positions.Add(temp);

        if (positions.Count > 100)  //remove 101th position
        {
            positions.RemoveAt(0);
        }

        foreach (Memb bMem in bandMem)  //band follows player
        {
            bMem.tra.position = positions[5].pos;
            bMem.spr.flipX = positions[5].flip;
        }
    }

    public void AddBandMember(GameObject _mem)  //adds member to the band
    {
        Memb temp = new Memb
        {
            tra = _mem.transform,
            spr = _mem.GetComponent<SpriteRenderer>()
        };

        bandMem.Add(temp);
    }
}
