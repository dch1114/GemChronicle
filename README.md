## 프로젝트 결과물 소개
![booth](https://github.com/dch1114/GemChronicle/assets/68212670/daae25c5-c323-4d17-8ac6-237878bd6350)

- 주인공이 젬의 비밀을 파해치며 보스를 물리치는 이야기

## 시연 영상
https://www.youtube.com/watch?v=FsWff4J_wcY(https://www.youtube.com/watch?v=FsWff4J_wcY)

## 사용된 기술 스택
![image](https://github.com/dch1114/GemChronicle/assets/68212670/7c9d32cb-22d3-4780-8c84-a51268b79491)

## 프로젝트 개요 및 목표

### 프로젝트 개요

- 우리게임은 “왕가의 비밀”이라는 판타지 세계를 배경으로 합니다. 이 세계에는 다양한 캐릭터, 몬스터, 그리고 마법이 존재합니다. 플레이어는 영웅으로 시작하여 여러 모험을 하면서 세계를 탐험하고, 임무를 수행하며 성장해나갑니다.

### 목표

- 탐험과 모험 : 플레이어가 "마법의 숲"을 탐험하는 동안 다양한 장소를 발견하고, 퀘스트를 완료하며 세계를 탐험할 수 있도록 합니다.
- 캐릭터 성장: 플레이어가 경험치를 획득하고 레벨업하여 능력치를 향상시키며, 젬을 획득하여 다양한 스킬을 배우고 특수 능력을 확보할 수 있도록 합니다.
- 실시간 전투 시스템 : 턴 기반이 아닌 실시간 전투 시스템을 개발하여 플레이어가 몬스터와 싸울 때 플레이어의 빠른 반응과 능력이 중요한 요소가 되도록 합니다.
- 다양한 스킬과 무기 : 플레이어는 다양한 스킬을 배우고 사용할 수 있으며, 또한 다양한 종류의 무기와 방어구를 사용하여 전투에 유리한 전략을 구사할 수 있습니다.
- 스토리텔링 : 게임에 깊이를 부여하기 위해 흥미진진한 스토리와 다양한 퀘스트를 제공합니다. 플레이어는 세계의 이야기를 탐험하고 주요 캐릭터와 상호작용할 수 있습니다.


## 기술적인 도전 과제

- **Excel2Json**
  ![image](https://github.com/dch1114/GemChronicle/assets/68212670/763a21c9-0429-49f5-9ebb-34f55e751458)
  : 게임 내 데이터(Item, NPC 데이터 등)을 엑셀로 관리하여 누구나 편히 수정가능하도록 관리
- **Singleton**<br>
  : 싱글톤 패턴을 사용하여 단일 인스턴스를 생성하고 해당 인스턴스 데이터의 공유 및 접근을 용이하게 함.
- **ScriptableObject**<br>
  : ScriptableObject을 사용하면 퀘스트 데이터를 쉽게 작성하고 관리할 수 있고 이를 통해 새로운 퀘스트를 추가하거나 수정하기가 용이하다. 또한 프로젝트 전체에서 재사용할 수 있으므로, 다양한 퀘스트를 만들 때 효율적으로 활용할 수 있다.
- **ObjectPooling**<br>
  : Instantiate와 Destroy 사용을 줄임으로써 메모리 사용량과 성능 저하를 줄일 수 있다. 이펙트 사운드, Enemy Skill, PlayerSkill에 사용하여 가비지 컬렉터의 발생을 줄임.
- **FSM** <br>
  ![image](https://github.com/dch1114/GemChronicle/assets/68212670/6bb64ae7-40c9-4b39-89de-509ecb000bf5) <br>
  : 캐릭터 상태 머신을 구현하여 각각 기본 상태 / 이동 상태 / 점프 상태 / 낙하 상태 / 공격 상태 / 사망 상태 를 전환하며 자연스럽게 상태가 변화하는 플레이어를 조작할 수 있게 하였다.

## 클라이언트 구조

### 🖥 씬 구조
![image](https://github.com/dch1114/GemChronicle/assets/68212670/1a2bd680-ee5f-4d6e-a4bb-9868e48d50d4)

### 🛠 매니저
![image](https://github.com/dch1114/GemChronicle/assets/68212670/b64cae99-770c-4a68-8197-aa00831edfee)

### 📗 퀘스트 & NPC
![image](https://github.com/dch1114/GemChronicle/assets/68212670/61356d38-464e-4456-ba11-3164424b5c5f)
![image](https://github.com/dch1114/GemChronicle/assets/68212670/b16c70c2-81c6-41ce-a2a3-e1006dfaaca2)


