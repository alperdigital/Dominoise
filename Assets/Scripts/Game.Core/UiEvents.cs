namespace Game.Core
{
    public static class UiEvents
    {
        public readonly struct CountdownShow { public readonly int seconds; public CountdownShow(int s){seconds=s;} }
        public readonly struct CountdownTick { public readonly int value; public CountdownTick(int v){value=v;} }
        public readonly struct CountdownHide {}

        public readonly struct SetCenterIcon {} // ileri de obje seti ekleyeceğiz
        public readonly struct ShowPercents { public readonly int p1,p2; public ShowPercents(int a,int b){p1=a;p2=b;} }
        public readonly struct UpdateScoreboard { public readonly int a,b; public UpdateScoreboard(int x,int y){a=x;b=y;} }

        public readonly struct ShowInsufficientGold {}
        public readonly struct HideInsufficientGold {}
    }
}
