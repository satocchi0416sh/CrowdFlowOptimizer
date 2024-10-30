# README: 人流シミュレーションプロジェクト

## プロジェクト概要

このプロジェクトでは、エージェント（人や物の動きのモデル）を使って、人の流れ（人流）をシミュレーションします。例えば、学校で100人が1つのドアから出る場合、どのくらい時間がかかるかを考えることができます。この研究では、「むしろ障害物を置いた方が、流れがスムーズになるのでは？」という仮説を検証するため、実験を行います。

シンプルな物理エンジンを使って、ボールやドアのようなモデルを作成し、エージェントがゴールに向かう際にどのように動くかを観察します。

## 使用技術一覧

<p style="display: inline">
  <img src="https://img.shields.io/badge/-Unity-000000.svg?logo=unity&style=for-the-badge">
  <img src="https://img.shields.io/badge/-C%23-239120.svg?logo=csharp&style=for-the-badge">
</p>

## 目次

1. [プロジェクトについて](#プロジェクトについて)
2. [システム要件](#システム要件)
3. [使用方法](#使用方法)
4. [コード説明](#コード説明)
5. [トラブルシューティング](#トラブルシューティング)

<p align="right">(<a href="#top">トップへ</a>)</p>

## プロジェクトについて

このプロジェクトでは、エージェントを動かすシミュレーションを使って、狭いドアや障害物のある環境での人流を観察します。エージェントは、スタート地点からゴール地点まで移動し、途中で他のエージェントや障害物に対応しながら進みます。この実験では、障害物を置くことが流れをスムーズにするのか、逆に遅くなるのかを確認します。

## システム要件

- Unity 2020.3 以降
- C# プログラミング

## 使用方法

1. Unityをインストールし、このリポジトリをクローンします。
2. プロジェクトをUnityで開き、シーンを実行します。
3. エージェントの数や障害物の配置を調整して、シミュレーションを試してみてください。

### シミュレーションの流れ

1. スタート地点にエージェントが現れる
2. エージェントはゴール地点を目指し、最短経路を探します
3. 障害物があると、エージェントは障害物を避けるルートを選びます
4. ゴールに到達するまでの時間を測定します

<p align="right">(<a href="#top">トップへ</a>)</p>

## コード説明

### Agentクラス

エージェントの動きを管理します。ターゲット（ゴール）に向けて移動するために、Unityの`NavMeshAgent`を使用しています。

```csharp
public class Agent : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    private Transform _target;

    public void InitializeAgent(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.position);
        }
    }
}
```

### SimulationManagerクラス

シミュレーション全体を管理します。複数のエージェントを生成し、彼らがゴール地点に向かうまでの時間を計測します。

```csharp
public class SimulationManager : MonoBehaviour
{
    private float _timeToExit;
    private List<Agent> _agents = new();

    [SerializeField] private GameObject _agentPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _numberOfAgents;
    [SerializeField] private Text _timeText;

    private void Start()
    {
        for (int i = 0; i < _numberOfAgents; i++)
        {
            GameObject agentGameObject = Instantiate(_agentPrefab, _spawnPoint.position, Quaternion.identity);
            Agent agent = agentGameObject.GetComponent<Agent>();
            agent.InitializeAgent(_target);
            _agents.Add(agent);
        }
    }

    private void Update()
    {
        _timeToExit += Time.deltaTime;
        _timeText.text = $"Time: {_timeToExit:F2}";
    }
}
```

## トラブルシューティング

- **エージェントがゴールに到達しない**: エージェントのターゲット（ゴール地点）が正しく設定されているか確認してください。
- **シミュレーションが動作しない**: Unityのバージョンが推奨されるバージョンか確認してください。また、`NavMeshAgent`がシーンに正しく配置されているか確認します。

<p align="right">(<a href="#top">トップへ</a>)</p>