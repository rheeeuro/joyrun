# 전시회용 미니게임 조이런(JoyRun)  
### 2020 하반기 산학인턴 프로젝트
   
   
      
      
## Trailer

[<img src="https://user-images.githubusercontent.com/47638660/91637142-2b647880-ea41-11ea-8eab-41a6c0b3069c.png" width="50%">](https://youtu.be/lgxhdsTtIAQ)


## 1. Title

전면 화면에 등장하는 장애물을 피하는 런 게임입니다.   
Azure Kinect Sensor를 이용한 사용자의 동작을 실시간 인식하여 플레이할 수 있습니다.   

## 2. Team member information

|Name|Email|GitHub
----|----|----|
김동재|kdjm1ru@naver.com|https://github.com/M1ru
이유로|eurohand@naver.com|https://github.com/rheeeuro
장휘준|law9560@gmail.com|https://github.com/201635848
  
## 3. Brief description

* Azure Kinect Sensor를 사용하여 사용자의 동작 인식
* 인식된 동작을 게임 내 Avatar에 적용
* 사용자는 이를 활용하여 게임 내 장애물을 피해 달리는 게임   

## 4. Required technologies for implementation

### 4.1 Tools
* Azure Kinect (SDK): Kinect Azure를 통해 유저의 3D 좌표를 알아내고 이를 통해 게임을 진행
* Unity: 전반적인 게임 제작
* Github: 협업툴로서의 기능
 

## 5. Primary  responsibility for parts

Name|Role
----|----
김동재|필드 구현, 펀치 구현 및 풍선 프리팹 제작, 환경 설정 구현, 버그 수정, 테스트 및 수치 조정
이유로|키넥트 연동, 발 좌표, 애니메이션, 코드 분할 및 리팩토링, 키넥트 인식 범위 및 속도 개선
장휘준|게임매니저 구현, 바닥 UI 구현, 캔버스 UI와 연결, 일시정지 기능 구현, 리소스 정리 및 요청, 적용