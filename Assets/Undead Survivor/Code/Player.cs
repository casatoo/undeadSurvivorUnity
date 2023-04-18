using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec; // 2차원 입력 변수
    public float speed; // 속도 변수
    Rigidbody2D rigid; // 물리작동 객체
    SpriteRenderer spriter; // spriteRenderer 객체
    Animator anim; // 에니메이터 객체
    public Scanner scanner;
    public Hand[] hands;

    void Awake()
    {
        // 변수 초기화
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    // InputSystem 도입으로 주석 처리
    // void Update()
    // {
    //     // 값 입력 받는 부분
    //     // getAxisRaw 명확한 컨트롤 가능
    //     inputVec.x = Input.GetAxisRaw("Horizontal");
    //     inputVec.y = Input.GetAxisRaw("Vertical");
    // }

    void FixedUpdate()
    {
        // //힘을 준다
        // rigid.AddForce(inputVec);

        // //속도제어
        // rigid.velocity = inputVec;

        // 입력값노멀라이즈 * 속도 * 물리프레임 하나당 소모된 시간
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        //위치이동
        rigid.MovePosition(rigid.position + nextVec);
    }

    // InputSystem 도입
    // vector2 타입의 값을 가져와서 set
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    // 업데이트 후 동작
    void LateUpdate()
    {
        // 에니메이션 speed 변경
        anim.SetFloat("Speed",inputVec.magnitude);

        // x 축 반전 방향전환
        if (inputVec.x != 0) {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
