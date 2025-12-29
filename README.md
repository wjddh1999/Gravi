# 🌌 Gravi

> **중력을 자유자재로 조작하여 복잡한 퍼즐을 해결하는 유니티 퍼즐 플랫포머**

[![](https://img.shields.io/badge/Unity-2022.3.15f-white?style=flat-square&logo=Unity)](https://unity.com/)
[![](https://img.shields.io/badge/Language-C%23-blue?style=flat-square&logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![](https://img.shields.io/badge/Platform-Windows-orange?style=flat-square)](https://www.microsoft.com)

---

## 📌 0. 프로젝트 개요

* **개발 목적**: 게임 'VVVVVV'의 메커니즘에서 영감을 받아, 플레이어뿐만 아니라 주변 오브젝트의 중력까지 제어하는 독창적인 퍼즐 기믹을 구현하고자 했습니다.
* **개발 기간**: 2025.04 ~ 2025.05 (약 2개월)
* **주요 기능**: 상하 중력 반전 시스템, 색상별 큐브 중력 조작, 물리 기반 큐브 잡기 및 발사 메커니즘.

---

## 🏗️ 1. 시스템 구조

### 🎮 Game Manager & UI
* **스테이지 선택 (Stage Selector)**: Enum 기반의 스테이지 관리 및 페이지를 넘기는 듯한 UI 연출 구현.
* **리소스 관리**: `Resources.Load<Sprite>`를 활용하여 스테이지 선택에 따른 프리뷰 이미지를 동적으로 교체합니다.

### 🕹️ 핵심 로직 (Core Logic)
* **PlayerController**: 플레이어의 중력 상태를 관리하며, 입력을 통해 `gravityScale`을 반전시켜 상하 이동을 제어합니다.
* **GravityCube**: 특정 범위 내 동일 색상의 큐브들을 탐색하고, 선택된 그룹의 중력을 조작하는 스크립트입니다.
* **InteractionSystem**: 레이캐스트와 트리거를 이용해 오브젝트를 잡거나, 물리적인 힘을 가해 발사하는 로직을 담당합니다.

---

## ⚙️ 2. 핵심 기능: 중력 조작

수동적인 움직임을 넘어 **'중력의 유기적 통제'**를 퍼즐의 핵심 요소로 구현했습니다.

### 🔄 캐릭터 중력 전환 (Gravity Flip)
1. **중력 반전**: `Space` 입력 시 캐릭터의 `gravityScale` 부호를 반전시켜 중력 방향을 바꿉니다.
2. **속도 보정**: `Vector2.Lerp`를 사용하여 중력 전환 시 발생하는 급격한 속도 변화를 제어하고 부드러운 전환을 유도합니다.
3. **시각적 피드백**: 중력 방향에 맞춰 캐릭터의 `localScale.y`를 반전시켜 현재 중력 상태를 직관적으로 전달합니다.

### 🧊 큐브 색상별 중력 조작 (Color-based Gravity Shift)
1. **모드 필터링**: Red, Blue, Green, Yellow 중 사용자가 선택한 색상 모드에 따라 조작 대상을 결정합니다.
2. **범위 탐색**: 플레이어 주변 일정 거리(약 15.5f) 내에 있는 동일 색상 큐브를 `Vector2.Distance`로 판별합니다.
3. **그룹 제어**: 조건에 맞는 큐브들의 중력만 일괄적으로 반전시켜, 특정 구역의 퍼즐을 해결하는 전략적 플레이를 유도합니다.

---

## 🗺️ 3. 스테이지 및 플레이 요소

### 📂 스테이지 선택 시스템
* **인터랙티브 UI**: 좌우 방향키 입력에 따라 중앙과 측면 패널이 애니메이션(`SetTrigger`)과 함께 전환되는 직관적인 UX를 제공합니다.
* **난이도 설계**: 스테이지가 진행됨에 따라 여러 색상의 큐브를 복합적으로 제어해야 하는 고난도 퍼즐이 배치되어 있습니다.

### 🎯 오브젝트 상호작용 (Grab & Shoot)
* **잡기 및 운반**: 큐브를 잡으면 `isTrigger`를 활성화하여 충돌을 제어하고, 중력을 0으로 만들어 플레이어와 함께 이동하게 합니다.
* **차징 발사**: `X`키를 누르는 시간에 비례해 `Power` 게이지가 상승하며, 키를 뗄 때 `AddForce`를 통해 누적된 힘만큼 큐브를 발사합니다.

---

## 📹 4. UI/UX 및 연출
* **Dynamic Feedback**: 중력 전환 시의 스프라이트 반전과 물리 효과를 통해 조작감을 극대화했습니다.
* **Visualized HUD**: 현재 선택된 조작 색상과 캐릭터의 상태를 실시간으로 UI에 반영하여 가독성을 높였습니다.

---

## 🔗 관련 링크 및 자료
* **📺 프로젝트 시연 영상**: [YouTube 바로가기](https://www.youtube.com/watch?v=nI-j9Wqd01U)
* **📂 실행프로그램 (EXE)**: [Google Drive 바로가기](https://drive.google.com/drive/folders/1FDwiJJEcWErBpVeXVGUfM66d5cC6MxXU?usp=drive_link)
* **💻 전체 소스 코드 (Unity Project)**: [Google Drive 바로가기](https://drive.google.com/drive/folders/12RSItqxwB_HI3rgrIiHkONH2WXSfAdBY?usp=drive_link)

---

### 🛠️ 개발 환경
* **Engine**: Unity 2022.3.15f
* **IDE**: Visual Studio 2022
* **Language**: C#
