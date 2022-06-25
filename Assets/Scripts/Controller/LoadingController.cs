using System;
using Core;
using Model;
using Plugins.Common;
using View;

namespace Controller
{
    public class LoadingController : ControllerBase
    {
        private static Observable<float> serviceLoadingProgress => Bootstrap.Instance.serviceLoadingProgress;
        public LoadingView view;
        public override ViewBase GetView => view;

        private LoadingModel model = new LoadingModel();

        private void Start()
        {
            serviceLoadingProgress.onValueChanged += OnLoadingServiceUpdate;
            OnLoadingServiceUpdate(serviceLoadingProgress.Value);
        }

        private void OnLoadingServiceUpdate(float progress)
        {
            SetProgress(progress);
        }

        private void OnDestroy()
        {
            serviceLoadingProgress.onValueChanged -= OnLoadingServiceUpdate;
        }

        public void SetLoadingText(string text)
        {
            model.loadingText = text;
            view.UpdateLoadingText(model.loadingText);
        }

        public void SetProgress(float progress)
        {
            model.progress = progress;
            view.UpdateLoadingProgress(model.progress);
        }
    }
}