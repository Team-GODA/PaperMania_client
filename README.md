# PaperMania_client
*페이퍼마니아 클라이언트 레포지토리입니다.*

## 정보
- 버전 : Unity 6000.0.50f1

## 에셋 관리법
- 모든 에셋은 `Resources`에 저장한다.
- 스프라이트 에셋은 `Resources` 내부 폴더 중 `Sprite Assets`에 저장한다.
- 스크립트 및 기타 에셋은 'Resources' 내부 폴더 중 `Script Assets`에 저장한다.
***예시***
```
Assets/
└── Resources/
    ├── Sprite Assets/
    └── Script Assets/
```

## 파일 관리법
- 스크립트 파일은 용도에 맞는 폴더에 넣어둔다.

***예시***
```
Assets/
│
├── Scripts/
│   ├── Interface/       ← 인터페이스 관련
│   ├── SO/              ← SO 관련
│   ├── Player/          ← 플레이어 관련
│   ├── Enemy/           ← 적 관련
│   └── UI/              ← UI 관련
```
